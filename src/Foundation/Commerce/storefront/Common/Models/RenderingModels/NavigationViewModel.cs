﻿//-----------------------------------------------------------------------
// <copyright file="NavigationViewModel.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the NavigationViewModel class.</summary>
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

using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Foundation.Commerce;
using Sitecore.Foundation.Commerce.Models.Search;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Reference.Storefront.Models.RenderingModels
{
    public class NavigationViewModel : RenderingModel
    {
        public List<Item> ChildCategories { get; private set; }

        public string StoreTitle
        {
            get
            {
                var home = Context.Database.GetItem(Context.Site.RootPath + Context.Site.StartItem);
                return home[StorefrontConstants.ItemFields.Title];
            }
        }

        public void Initialize(Rendering rendering, CategorySearchResults childCategories)
        {
            base.Initialize(rendering);

            if (childCategories != null)
            {
                ChildCategories = childCategories.CategoryItems;
            }
        }
    }
}