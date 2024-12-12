
# Usefull SQL syntax


--=*=-- periodic cleanup job (e.g., a SQL Server Agent Job or background service) to hard delete soft-deleted records older than a certain period:

```
DELETE FROM Tbl_MAP_UserRole
WHERE IsDelete = 1 AND DATEDIFF(DAY, ModifiedDate, GETDATE()) > 30;
```


--=*=-- table structure

```
select *
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME='Table_Name'
```


--=*=-- Seeing Foriegn_Key and its referencing tables

```
SELECT 
    fk.name AS ForeignKey,
    tp.name AS ReferencingTable,
    cp.name AS ReferencingColumn,
    tr.name AS ReferencedTable,
    cr.name AS ReferencedColumn
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.tables AS tp ON fkc.parent_object_id = tp.object_id
INNER JOIN sys.columns AS cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.tables AS tr ON fkc.referenced_object_id = tr.object_id
INNER JOIN sys.columns AS cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
WHERE 1 = 1
    AND tr.name = 'Table_Name';
```


--=*=-- Droping Foriegn Key constraint from referencing table

```
alter table Table_Name
drop Constraint Foreign_Key_Constraint_Name
```





