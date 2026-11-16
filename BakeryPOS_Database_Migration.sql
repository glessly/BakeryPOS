-- =============================================================================
-- BakeryPOS Database Schema & Migration Script
-- Ensures the Sales table matches:
-- SaleID, CashierUsername, SaleDate, TotalAmount, CashAmount, ChangeAmount,
-- Status, VoidReason, VoidedBy, VoidedDate, RowVer
-- =============================================================================

USE [BakeryPOS_DB];
GO

IF OBJECT_ID('dbo.Sales', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Sales
    (
        SaleID          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CashierUsername NVARCHAR(50)      NOT NULL,
        SaleDate        DATETIME          NOT NULL CONSTRAINT DF_Sales_SaleDate DEFAULT (GETDATE()),
        TotalAmount     DECIMAL(18,2)     NOT NULL,
        CashAmount      DECIMAL(18,2)     NOT NULL,
        ChangeAmount    DECIMAL(18,2)     NOT NULL,
        Status          NVARCHAR(20)      NOT NULL CONSTRAINT DF_Sales_Status DEFAULT ('Completed'),
        VoidReason      NVARCHAR(255)     NULL,
        VoidedBy        NVARCHAR(50)      NULL,
        VoidedDate      DATETIME          NULL,
        RowVer          ROWVERSION        NOT NULL
    );
END
ELSE
BEGIN
    -- Drop unused Subtotal column on Sales if present (along with any default constraint)
    IF COL_LENGTH('dbo.Sales', 'Subtotal') IS NOT NULL
    BEGIN
        DECLARE @dfSubtotal NVARCHAR(128);
        SELECT @dfSubtotal = dc.name
        FROM sys.default_constraints dc
        INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE dc.parent_object_id = OBJECT_ID('dbo.Sales') AND c.name = 'Subtotal';

        IF @dfSubtotal IS NOT NULL
            EXEC('ALTER TABLE dbo.Sales DROP CONSTRAINT [' + @dfSubtotal + ']');

        ALTER TABLE dbo.Sales DROP COLUMN Subtotal;
    END

    -- Drop unused Discount column on Sales if present (along with any default constraint)
    IF COL_LENGTH('dbo.Sales', 'Discount') IS NOT NULL
    BEGIN
        DECLARE @dfDiscount NVARCHAR(128);
        SELECT @dfDiscount = dc.name
        FROM sys.default_constraints dc
        INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE dc.parent_object_id = OBJECT_ID('dbo.Sales') AND c.name = 'Discount';

        IF @dfDiscount IS NOT NULL
            EXEC('ALTER TABLE dbo.Sales DROP CONSTRAINT [' + @dfDiscount + ']');

        ALTER TABLE dbo.Sales DROP COLUMN Discount;
    END

    -- Ensure all required columns exist on Sales
    IF COL_LENGTH('dbo.Sales', 'Status') IS NULL
        ALTER TABLE dbo.Sales ADD Status NVARCHAR(20) NOT NULL CONSTRAINT DF_Sales_Status DEFAULT ('Completed');

    IF COL_LENGTH('dbo.Sales', 'VoidReason') IS NULL
        ALTER TABLE dbo.Sales ADD VoidReason NVARCHAR(255) NULL;

    IF COL_LENGTH('dbo.Sales', 'VoidedBy') IS NULL
        ALTER TABLE dbo.Sales ADD VoidedBy NVARCHAR(50) NULL;

    IF COL_LENGTH('dbo.Sales', 'VoidedDate') IS NULL
        ALTER TABLE dbo.Sales ADD VoidedDate DATETIME NULL;

    IF COL_LENGTH('dbo.Sales', 'RowVer') IS NULL
        ALTER TABLE dbo.Sales ADD RowVer ROWVERSION NOT NULL;
END
GO

