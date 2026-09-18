-- Enforces only 1 Primary salesperson for a district.
-- Created as index to better support other business rules in the future.
CREATE UNIQUE INDEX [UX_DistrictSalesperson_OnePrimary]
    ON [dbo].[DistrictSalesperson] ([DistrictId])
    WHERE [Role] = 'Primary';