using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using TuddalDiscordBot.Storage.Implementation;

namespace TuddalDiscordBot.Discord.Implementation.Commands.Modules.Preconditions
{

    // Inherit from PreconditionAttribute
    public class PermissionCheckerAttribute : PreconditionAttribute
    {
        // Create a field to store the specified name
        private readonly ILogger _logger;
        private JsonStorage _jsonStorage;

        // Create a constructor so the name can be specified
        public PermissionCheckerAttribute()
        {
            _logger = new Logger();
            _jsonStorage = new JsonStorage();
        }

        // Override the CheckPermissions method
        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            try
            {
                var rootObject = _jsonStorage.RestoreObject<RootObject>(context.User.Id.ToString());

                var x = command.Module.Group;
                var y = command.Name;
                bool result = rootObject.permissions.CommandGroup<object>("response").CommandProperty("ping");
                if (result)
                {
                    //YAY!!!
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            _logger.Log($"User: {context.User.Id} failed to execute {command.Name}", "CommandWarning");
            await context.Channel.SendMessageAsync($"User needs `permissions.{command.Module.Group}.{command.Name}`");
            return await Task.FromResult(
                PreconditionResult.FromError("Fail"));
        }
    }

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



}
