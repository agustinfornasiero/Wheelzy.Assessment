USE WheelzyDb;
GO

IF OBJECT_ID('dbo.Orders') IS NULL
BEGIN
    CREATE TABLE dbo.Orders
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NOT NULL,
        StatusId INT NOT NULL,
        OrderDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        IsActive BIT NOT NULL DEFAULT 1,
        Amount DECIMAL(18,2) NOT NULL DEFAULT 0,

        CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id),
        CONSTRAINT FK_Orders_Statuses FOREIGN KEY (StatusId) REFERENCES dbo.Statuses(Id)
    );

    CREATE INDEX IX_Orders_CustomerId ON dbo.Orders(CustomerId);
    CREATE INDEX IX_Orders_StatusId ON dbo.Orders(StatusId);
    CREATE INDEX IX_Orders_OrderDate ON dbo.Orders(OrderDate);
END

INSERT INTO dbo.Orders (CustomerId, StatusId, OrderDate, IsActive, Amount)
VALUES 
(1, 1, DATEADD(DAY, -10, SYSUTCDATETIME()), 1, 1500), -- New
(1, 2, DATEADD(DAY, -5, SYSUTCDATETIME()), 1, 2000), -- Assigned
(1, 3, DATEADD(DAY, -1, SYSUTCDATETIME()), 0, 1800); -- Picked Up (inactivo)