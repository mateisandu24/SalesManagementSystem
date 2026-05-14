USE SalesManagementSystem;
GO

-- 1. Ștergem mai întâi produsele din comenzi (tabelul dependent)
DELETE FROM OrderItems;

-- 2. Ștergem comenzile propriu-zise
DELETE FROM Orders;

-- 3. Resetăm contoarele de Identity ca id-urile să înceapă iar de la 1
DBCC CHECKIDENT ('OrderItems', RESEED, 0);
DBCC CHECKIDENT ('Orders', RESEED, 0);
GO
