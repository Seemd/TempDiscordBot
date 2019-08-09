        public class RootObject
    {
        public Permissions permissions { get; set; }
        public int points { get; set; }
    }

    public class Permissions
    {
        public Response response { get; set; }
        public Admin admin { get; set; }
    }
    public class Response
    {
        public bool ping { get; set; }
        public bool helloWorld { get; set; }
    }

    public class Admin
    {
        public bool ban { get; set; }
    }
    public static class PermissionsExtensions
    {
        public static T CommandGroup<T>(this Permissions permissions, string commandGroup)
        {
            PropertyInfo commandGroupProperty = typeof(Permissions).GetProperty(commandGroup);
            return (T)(commandGroupProperty.GetValue(permissions));
        }
        public static bool CommandProperty<T>(this T commandGroup, string commandProperty)
        {
            PropertyInfo commandPropertyProperty = typeof(T).GetProperty(commandProperty);
            return (bool)(commandPropertyProperty.GetValue(commandGroup));
        }
    }
                bool result = rootObject.permissions.CommandGroup<object>("response").CommandProperty("ping");
                if (result)
                {
                    //YAY!!!
                }
