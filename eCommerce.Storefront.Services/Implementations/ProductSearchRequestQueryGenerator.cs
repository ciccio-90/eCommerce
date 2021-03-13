using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Services.Messaging.ProductCatalogService;
using LinqKit;

namespace eCommerce.Storefront.Services.Implementations
{
    public static class ProductSearchRequestQueryGenerator
    {
        public static Expression<Func<Product, bool>> CreateQueryFor(GetProductsByCategoryRequest getProductsByCategoryRequest)
        {
            Expression<Func<Product, bool>> productQuery = null;
            List<Expression<Func<Product, bool>>> colorQuery = new List<Expression<Func<Product, bool>>>();
            List<Expression<Func<Product, bool>>> brandQuery = new List<Expression<Func<Product, bool>>>();
            List<Expression<Func<Product, bool>>> sizeQuery = new List<Expression<Func<Product, bool>>>();
            Expression<Func<Product, bool>> categoryQuery = null;

            foreach (int id in getProductsByCategoryRequest.ColorIds)
            {
                colorQuery.Add(p => p.Title.Color.Id == id);
            }

            if (colorQuery.Count > 0)
            {
                foreach (Expression<Func<Product, bool>> predicate in colorQuery)
                {
                    if (productQuery == null)
                    {
                        productQuery = predicate;
                    }
                    else
                    {
                        productQuery = PredicateBuilder.Or(productQuery, predicate);
                    }
                }
            }

            foreach (int id in getProductsByCategoryRequest.BrandIds)
            {
                brandQuery.Add(p => p.Title.Brand.Id == id);
            }

            if (brandQuery.Count > 0)
            {
                foreach (Expression<Func<Product, bool>> predicate in brandQuery)
                {
                    if (productQuery == null)
                    {
                        productQuery = predicate;
                    }
                    else
                    {
                        productQuery = PredicateBuilder.Or(productQuery, predicate);
                    }
                }
            }

            foreach (int id in getProductsByCategoryRequest.SizeIds)
            {
                sizeQuery.Add(p => p.Size.Id == id);
            }

            if (sizeQuery.Count > 0)
            {
                foreach (Expression<Func<Product, bool>> predicate in sizeQuery)
                {
                    if (productQuery == null)
                    {
                        productQuery = predicate;
                    }
                    else
                    {
                        productQuery = PredicateBuilder.Or(productQuery, predicate);
                    }
                }
            }

            categoryQuery = (p => p.Title.Category.Id == getProductsByCategoryRequest.CategoryId);
            
            if (productQuery == null)
            {
                productQuery = categoryQuery;
            }
            else
            {
                productQuery = PredicateBuilder.Or(productQuery, categoryQuery);
            }

            return productQuery;
        }
    }
}