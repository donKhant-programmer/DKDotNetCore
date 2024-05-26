namespace DKDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } = 
            @"select po.*, p.Pizza, p.Price from [dbo].[Table_PizzaOrder] po
            inner join Table_Pizza p on p.PizzaId = po.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";

        public static string PizzaOrderDetailQuery { get; } = 
            @"select ped.*, pe.PizzaExtraName, pe.Price from [dbo].[Table_PizzaOrderDetails] ped
            inner join Table_PizzaExtra pe on pe.PizzaExtraId = ped.PizzaExtraId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
    }
}
