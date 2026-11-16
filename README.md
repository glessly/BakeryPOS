# Bread Basket POS (`BakeryPOS`)
A Windows Forms (C# .NET) Point of Sale and Inventory Management System built for **Bread Basket Bakeshop (Buhangin, Davao City)**, 
backed by a normalized 3NF SQL Server database (`BakeryPOS_DB`).

## Key Features
- **Point of Sale (POS) Checkout:** Live stock validation, item cart management, cash/change calculation,
atomic multi-table checkout (`SqlTransaction`), and formatted receipt generation (`Receipt_ORD-XXXX.txt`).
- **Master File Management:** Full CRUD, search/filtering, and soft-delete/reactivation (`IsActive`) for Products, Categories, and Suppliers.
- **Inventory & Restocking:** Supplier Stock-In recording (`StockIns` / `StockInItems`) with automatic stock updates and void reversal support.
- **Sales History & Transaction Controls:** Order lookup, item-level sale editing (`frmEditSale`), and transaction voiding (`frmSales`) with
automatic inventory restoration.
- **Reports & Analytics:** Filterable Sales Report, Inventory Valuation Report, AI Smart Reorder Forecast, and Cashier End-of-Shift Report with print preview support.
- **Security & Administration:** Salted PBKDF2-SHA256 password hashing (`SecurityHelper`), 3-attempt login lockout, Role-Based Access Control (`Admin` vs `Cashier`), Audit Logs (`AuditLogs`), optimistic concurrency (`ROWVERSION`), and SQL Server Backup/Restore.

## Tech Stack
- **Frontend / Desktop App:** C# Windows Forms (.NET Framework)
- **Database:** Microsoft SQL Server (`BakeryPOS_DB`, 3NF Schema)
- **Data Access:** ADO.NET with 100% parameterized queries (`SqlParameter`)

## Database Setup
1. Open SQL Server Management Studio (SSMS) and run `BakeryPOS_Database_Migration.sql` (or restore `BakeryPOS_DB.bak`).
2. Verify the connection string in `BakeryPOS/App.config`:

```xml
<connectionStrings>
    <add name="BakeryPOS_DB"
         connectionString="Server=.;Database=BakeryPOS_DB;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

3. Open `BakeryPOS.slnx` in Visual Studio and press **F5** to run.
## Default Credentials (Testing & Demo)

| Role | Username | Password |
|---|---|---|
| Administrator | `admin` | `admin123` |
| Cashier / Staff | `glessly` | `glessly18` |
| Cashier / Staff | `staff` | `staff123` |

## Development Team
**Glessly Aguilar**
- Core POS Architecture, Security & RBAC (`SecurityHelper`, `frmLogin`, `frmManageAccounts`).
- Master Data CRUD (`frmProducts`, `frmCategories`, `frmSuppliers`).
- Stock-In, Audit Logs, and Database Backup/Restore.
- Testing Validation and System Documentation.

**Adrianne Sindian**
- Sales History & Transaction Management (`frmSales`, `frmSaleDetails`, `frmEditSale`).
- Reporting & End-of-Shift Analytics (`frmReports`, `frmEndOfShiftReport`).
