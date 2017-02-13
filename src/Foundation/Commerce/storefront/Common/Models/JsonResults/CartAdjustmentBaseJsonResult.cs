﻿//-----------------------------------------------------------------------
// <copyright file="CartAdjustmentBaseJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the CartAdjustmentBaseJsonResult class.</summary>
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

using Sitecore.Commerce.Entities.Carts;
using Sitecore.Diagnostics;

namespace Sitecore.Reference.Storefront.Models.JsonResults
{
    public class CartAdjustmentBaseJsonResult : BaseJsonResult
    {
        public CartAdjustmentBaseJsonResult(CartAdjustment adjustment)
        {
            Assert.ArgumentNotNull(adjustment, "adjustment");
            Amount = adjustment.Amount.ToString("C", Context.Language.CultureInfo);
            Description = adjustment.Description;
            IsCharge = adjustment.IsCharge;
            LineNumber = adjustment.LineNumber;
            Percentage = adjustment.Percentage;
        }

        public string Amount { get; set; }

        public string Description { get; set; }

        public bool IsCharge { get; set; }

        public uint LineNumber { get; set; }

        public float Percentage { get; set; }
    }
}