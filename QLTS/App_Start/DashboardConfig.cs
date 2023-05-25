using System.Web.Routing;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DashboardWeb.Mvc;
using DevExpress.DataAccess.EntityFramework;
using DevExpress.DataAccess.Sql;
using QLTS.Models;

namespace QLTS
{
    public class DashboardConfig
    {
        public static void RegisterService(RouteCollection routes)
        {
            routes.MapDashboardRoute("dashboardControl", "DefaultDashboard");

            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            DashboardConfigurator.Default.SetDashboardStorage(dashboardFileStorage);

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

            DashboardEFDataSource efDataSource = new DashboardEFDataSource("EF Data Source");
            efDataSource.ConnectionParameters = new EFConnectionParameters(typeof(QLTSDbContext));
            dataSourceStorage.RegisterDataSource("efDataSource", efDataSource.SaveToXml());

            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "DefaultConnection");
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStorage);
        }
    }
}