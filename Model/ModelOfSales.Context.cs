﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Model
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class ModelOfSalesContainer : DbContext
{
    public ModelOfSalesContainer()
        : base("name=ModelOfSalesContainer")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<SaleInfo> SaleInfoSet { get; set; }

    public virtual DbSet<Product> ProductSet { get; set; }

    public virtual DbSet<Client> ClientSet { get; set; }

    public virtual DbSet<Manager> ManagerSet { get; set; }

}

}

