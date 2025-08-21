-- Replace with your user's ID
DECLARE @UserId UNIQUEIDENTIFIER = 'f6044bde-18df-4c3c-a6c4-281903a92082';

-- 1. Remove from user roles
DELETE FROM AspNetUserRoles WHERE UserId = @UserId;

-- 2. Remove from user claims
DELETE FROM AspNetUserClaims WHERE UserId = @UserId;

-- 3. Remove from user logins
DELETE FROM AspNetUserLogins WHERE UserId = @UserId;

-- 4. Remove from user tokens
DELETE FROM AspNetUserTokens WHERE UserId = @UserId;

-- 5. Delete the user itself
DELETE FROM AspNetUsers WHERE Id = @UserId;
