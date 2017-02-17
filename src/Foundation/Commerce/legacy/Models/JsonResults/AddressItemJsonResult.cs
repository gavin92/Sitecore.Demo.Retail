﻿//-----------------------------------------------------------------------
// <copyright file="AddressItemJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the AddressItemJsonResult class.</summary>
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

using Sitecore.Commerce.Connect.CommerceServer.Orders.Models;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Services;

namespace Sitecore.Reference.Storefront.Models.JsonResults
{
    public class AddressItemJsonResult : AddressItemBaseJsonResult
    {
        public AddressItemJsonResult()
        {
        }

        public AddressItemJsonResult(ServiceProviderResult result)
            : base(result)
        {
        }

        public override void Initialize(Party address)
        {
            base.Initialize(address);
            Name = ((CommerceParty) address).Name;
            IsPrimary = ((CommerceParty) address).IsPrimary;
        }
    }
}