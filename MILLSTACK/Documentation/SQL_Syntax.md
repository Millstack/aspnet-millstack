
# Usefull SQL syntax

```

--=*=-- periodic cleanup job (e.g., a SQL Server Agent Job or background service) to hard delete soft-deleted records older than a certain period:

DELETE FROM Tbl_MAP_UserRole
WHERE IsDelete = 1 AND DATEDIFF(DAY, ModifiedDate, GETDATE()) > 30;
```