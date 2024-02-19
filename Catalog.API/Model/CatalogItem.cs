﻿namespace Catalog.API.Model
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }
    }
}
