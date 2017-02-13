﻿//-----------------------------------------------------------------------
// <copyright file="ProductSearchResultViewModel.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
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
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Foundation.Commerce;
using Sitecore.Foundation.Commerce.Models;
using Sitecore.Mvc;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Reference.Storefront.Models
{
    public class ProductSearchResultViewModel : RenderingModel
    {
        public ProductSearchResultViewModel()
        {
            Products = new List<ProductViewModel>();
            DisplayName = string.Empty;
        }

        public string DisplayName { get; set; }

        public Item NamedSearchItem { get; set; }

        public HtmlString NamedSearchItemDisplayNameRender
        {
            get { return PageContext.Current.HtmlHelper.Sitecore().Field(StorefrontConstants.KnownFieldNames.Title, NamedSearchItem); }
        }

        public List<ProductViewModel> Products { get; set; }

        public virtual void Initialize(Rendering rendering, SearchResults searchResult)
        {
            base.Initialize(rendering);

            if (searchResult == null)
            {
                return;
            }

            DisplayName = searchResult.DisplayName;
            NamedSearchItem = searchResult.NamedSearchItem;
            Products = new List<ProductViewModel>();
            foreach (var child in searchResult.SearchResultItems)
            {
                var productModel = new ProductViewModel(child);
                productModel.Initialize(Rendering);
                Products.Add(productModel);
            }
        }
    }
}