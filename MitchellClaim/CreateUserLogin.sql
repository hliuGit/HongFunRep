use MTL_ClaimData
Go
CREATE LOGIN ClaimService With PASSWORD='bpqa2zJPuedXa'

GO
CREATE USER ClaimService FOR LOGIN ClaimService
Go
ALTER ROLE db_datareader ADD MEMBER ClaimService
GO
ALTER ROLE db_datawriter ADD MEMBER ClaimService
Go
GRANT EXECUTE TO [ClaimService]
Go