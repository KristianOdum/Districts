-- Salespersons (Must come before Districts due to primary)
INSERT INTO dbo.Salesperson (EmployeeNumber, Name)
VALUES ('EMP00120250601', N'Julie Jensen'),
       ('EMP00120200701', N'Søren Andreasen'),
       ('EMP00220200701', N'Frederik Hansen'),
       ('EMP00120120301', N'Johanne Schmidt Larsen'),
       ('EMP00120261101', N'Kristian Nielsen');


-- Districts
INSERT INTO dbo.District (Name, PrimarySalespersonId)
SELECT 'Northern Denmark', Id
FROM dbo.Salesperson
WHERE EmployeeNumber = 'EMP00120250601'

UNION ALL

SELECT 'Southern Denmark', Id
FROM dbo.Salesperson
WHERE EmployeeNumber = 'EMP00220200701'

UNION ALL

SELECT 'Central Denmark', Id
FROM dbo.Salesperson
WHERE EmployeeNumber = 'EMP00120120301'

UNION ALL

SELECT 'Zealand', Id
FROM dbo.Salesperson
WHERE EmployeeNumber = 'EMP00120200701';


-- Stores  -> https://guide.michelin.com/en/dk/north-denmark/aalborg/restaurants
INSERT INTO dbo.Store (Name, DistrictId)
SELECT 'Alimentum', Id
FROM dbo.District
WHERE Name = 'Northern Denmark';

INSERT INTO dbo.Store (Name, DistrictId)
SELECT 'Bach & Nurup', Id
FROM dbo.District
WHERE Name = 'Northern Denmark';

INSERT INTO dbo.Store (Name, DistrictId)
SELECT N'Frederikshøj', Id
FROM dbo.District
WHERE Name = 'Central Denmark';

INSERT INTO dbo.Store (Name, DistrictId)
SELECT 'Syttende', Id
FROM dbo.District
WHERE Name = 'Southern Denmark';

INSERT INTO dbo.Store (Name, DistrictId)
SELECT 'Geranium', Id
FROM dbo.District
WHERE Name = 'Zealand';

INSERT INTO dbo.Store (Name, DistrictId)
SELECT 'Kadeau', Id
FROM dbo.District
WHERE Name = 'Zealand';


-- District / Salesperson assignments
INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Northern Denmark'
  AND s.EmployeeNumber = 'EMP00120250601';

INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Northern Denmark'
  AND s.EmployeeNumber = 'EMP00120200701';

INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Southern Denmark'
  AND s.EmployeeNumber = 'EMP00220200701';

INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Central Denmark'
  AND s.EmployeeNumber = 'EMP00120120301';

INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Central Denmark'
  AND s.EmployeeNumber = 'EMP00120261101';

INSERT INTO dbo.DistrictSecondarySalesperson (DistrictId, SalespersonId)
SELECT d.Id, s.Id
FROM dbo.District d
         CROSS JOIN dbo.Salesperson s
WHERE d.Name = 'Zealand'
  AND s.EmployeeNumber = 'EMP00120200701';


-- Salesperson / Store assignments
INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00120250601'
  AND st.Name = 'Alimentum';

INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00120200701'
  AND st.Name IN ('Alimentum', 'Bach & Nurup');

INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00220200701'
  AND st.Name = 'Syttende';

INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00120120301'
  AND st.Name = N'Frederikshøj';

INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00120261101'
  AND st.Name = N'Frederikshøj';

INSERT INTO dbo.SalespersonStore (SalespersonId, StoreId)
SELECT s.Id, st.Id
FROM dbo.Salesperson s
         CROSS JOIN dbo.Store st
WHERE s.EmployeeNumber = 'EMP00120200701'
  AND st.Name IN ('Geranium', 'Kadeau');