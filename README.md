How often do you write code like this?
```csharp
public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserStatus Status { get; set; }
    public string Role { get; set; }
    public UserPermission Permission { get; set; }
}
```

```csharp
static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    if (currentUser.Status == UserStatus.Active && currentUser.Role == UserRole.Admin && currentUser.Permission.HasFlag(UserPermission.Write)) 
    {
        // ... some implementation
    }
}
```

Extracting methods from conditional

```csharp
static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    if (IsActive(currentUser) && IsAdmin(currentUser) && CanEdit(currentUser))
    {
        // ... some implementation
    }
}

private static bool CanEdit(User currentUser)
{
    return currentUser.Permission.HasFlag(UserPermission.Write);
}

private static bool IsAdmin(User currentUser)
{
    return currentUser.Role == UserRole.Admin;
}

private static bool IsActive(User currentUser)
{
    return currentUser.Status == UserStatus.Active;
}
```



```csharp
static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    var activeAdminCanEdit = IsActive(currentUser) && IsAdmin(currentUser) && CanEdit(currentUser);

    if (activeAdminCanEdit)
    {
        // ... some implementation
    }
}

private static bool CanEdit(User currentUser)
{
    return currentUser.Permission.HasFlag(UserPermission.Write);
}

private static bool IsAdmin(User currentUser)
{
    return currentUser.Role == UserRole.Admin;
}

private static bool IsActive(User currentUser)
{
    return currentUser.Status == UserStatus.Active;
}
```

```csharp
public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserStatus Status { get; set; }
    public string Role { get; set; }
    public UserPermission Permission { get; set; }

    public bool IsAdmin() => this.Role == UserRole.Admin;

    public bool IsActive() => this.Status == UserStatus.Active;

    public bool CanEdit() => this.Permission.HasFlag(UserPermission.Write);

    public bool ActiveAdminCanEdit() => IsActive() && IsAdmin() && CanEdit();
}

static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    if (currentUser.ActiveAdminCanEdit())
    {
        // ... some implementation
    }
}
```
Extract conditional logic to explicit classes
Single responsibility principle

```csharp
public class UserIsActiveAdmin
{
    private readonly User _user;
    public AdminUser(User user)
    {
        _user = user;
    }

    private bool IsAdmin() => _user.Role == UserRole.Admin;

    private bool IsActive() => _user.Status == UserStatus.Active;
    
    public bool Invoke() => IsActive() && IsAdmin();
    
}

static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    var activeAdminCanEdit = (new UserIsActiveAdmin(currentUser)).Invoke() && currentUser.CanEdit
}
```

```csharp
public interface IConditionalPipe
{
    bool Check();
}

public class UserIsAdminConditionalPipe : IConditionalPipe
{
    private readonly User _user;
    public UserIsAdminConditionalPipe(User user)
    {
        _user = user;
    }

    public bool Check()
    {
        return _user.Role == UserRole.Admin;
    }
}

public class UserIsActiveConditionalPipe : IConditionalPipe
{
    private readonly User _user;
    public UserIsActiveConditionalPipe(User user)
    {
        _user = user;
    }

    public bool Check()
    {
        return _user.Status == UserStatus.Active;
    }
}

public class UserCanEditConditionalPipe : IConditionalPipe
{
    private readonly User _user;
    public UserCanEditConditionalPipe(User user)
    {
        _user = user;
    }

    public bool Check()
    {
        return _user.Permission.HasFlag(UserPermission.Write);
    }
}

public class ConditionalPipes
{
    public static bool Check(params IConditionalPipe[] pipes)
    {
        return pipes.All(pipe => pipe.Check());
    }
}

static void Main(string[] args)
{
    var currentUser = new User()
    {
        FirstName = "Izuku",
        LastName = "Midoriya",
        Status = UserStatus.Active,
        Role = UserRole.Admin,
        Permission = UserPermission.Read | UserPermission.Write
    };

    var activeAdminCanEdit = ConditionalPipes.Check(
        new UserIsAdminConditionalPipe(currentUser),
        new UserIsActiveConditionalPipe(currentUser);
        new UserCanEditConditionalPipe(currentUser)
    );
}

```

