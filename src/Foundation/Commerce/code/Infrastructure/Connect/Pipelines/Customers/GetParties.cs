﻿//-----------------------------------------------------------------------
// <copyright file="GetParties.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Pipeline processor used to retrieve parties (addresses) from CS user profiles.</summary>
//-----------------------------------------------------------------------
// Copyright 2016 Sitecore Corporation A/S
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
// except in compliance with the License. You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the 
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
// either express or implied. See the License for the specific language governing permissions 
// and limitations under the License.
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CommerceServer.Core.Runtime.Profiles;
using Sitecore.Commerce.Connect.CommerceServer.Orders.Models;
using Sitecore.Commerce.Connect.CommerceServer.Pipelines;
using Sitecore.Commerce.Connect.CommerceServer.Profiles.Models;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Pipelines;
using Sitecore.Commerce.Services.Customers;
using Sitecore.Diagnostics;
using Sitecore.Foundation.Commerce.Connect.Pipelines.Arguments;

namespace Sitecore.Foundation.Commerce.Connect.Pipelines.Customers
{
    public class GetParties : CustomerPipelineProcessor
    {
        public GetParties([NotNull] IEntityFactory entityFactory)
        {
            Assert.ArgumentNotNull(entityFactory, "entityFactory");

            EntityFactory = entityFactory;
        }

        public IEntityFactory EntityFactory { get; set; }

        public override void Process(ServicePipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentCondition(args.Request is GetPartiesRequest, "args.Request", "args.Request is GetPartiesRequest");
            Assert.ArgumentCondition(args.Result is GetPartiesResult, "args.Result", "args.Result is GetPartiesResult");

            var request = (GetPartiesRequest) args.Request;
            var result = (GetPartiesResult) args.Result;
            Assert.ArgumentNotNull(request.CommerceCustomer, "request.CommerceCustomer");

            var partyList = new List<Party>();

            Profile customerProfile = null;
            var response = GetCommerceUserProfile(request.CommerceCustomer.ExternalId, ref customerProfile);
            if (!response.Success)
            {
                result.Success = false;
                response.SystemMessages.ToList().ForEach(m => result.SystemMessages.Add(m));
                return;
            }

            var preferredAddress = customerProfile["GeneralInfo.preferred_address"].Value as string;

            var profileValue = customerProfile["GeneralInfo.address_list"].Value as object[];
            if (profileValue != null)
            {
                var e = profileValue.Select(i => i.ToString());
                var addresIdsList = new ProfilePropertyListCollection<string>(e);
                foreach (var addressId in addresIdsList)
                {
                    Profile commerceAddress = null;
                    response = GetCommerceAddressProfile(addressId, ref commerceAddress);
                    if (!response.Success)
                    {
                        result.Success = false;
                        response.SystemMessages.ToList().ForEach(m => result.SystemMessages.Add(m));
                        return;
                    }

                    var newParty = EntityFactory.Create<CommerceParty>("Party");
                    var requestTorequestToEntity = new TranslateCommerceAddressProfileToEntityRequest(commerceAddress, newParty);
                    PipelineUtility.RunCommerceConnectPipeline<TranslateCommerceAddressProfileToEntityRequest, CommerceResult>(Constants.Pipelines.TranslateCommerceAddressProfileToEntity, requestTorequestToEntity);

                    if (!string.IsNullOrWhiteSpace(preferredAddress) && preferredAddress.Equals(newParty.ExternalId, StringComparison.OrdinalIgnoreCase))
                        newParty.IsPrimary = true;

                    var address = requestTorequestToEntity.DestinationParty;

                    partyList.Add(address);
                }
            }

            result.Parties = partyList.AsReadOnly();
        }
    }
}