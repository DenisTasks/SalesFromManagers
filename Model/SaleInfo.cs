
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
    using System.Collections.Generic;
    
public partial class SaleInfo
{

    public int SaleInfoId { get; set; }

    public int ProductId { get; set; }

    public int ClientId { get; set; }

    public int ManagerId { get; set; }

    public string DateOfSale { get; set; }



    public virtual Product Product { get; set; }

    public virtual Client Client { get; set; }

    public virtual Manager Manager { get; set; }

}

}
