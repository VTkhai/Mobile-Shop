using MobileShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShopOnline.Controllers.Iterator
{
    public class FavoriteProductIterator : IFavoriteProductIterator
    {
        private List<FavoriteProduct> _favoriteProducts;
        private int _index = 0;

        public FavoriteProductIterator(List<FavoriteProduct> favoriteProducts)
        {
            _favoriteProducts = favoriteProducts;
        }

        public bool HasNext()
        {
            return _index < _favoriteProducts.Count;
        }

        public FavoriteProduct Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more favorite products.");
            }
            return _favoriteProducts[_index++];
        }
    }

}