                var rootObject = _jsonStorage.RestoreObject<RootObject>(context.User.Id.ToString());
                var x = command.Module.Group;
                var y = command.Name;
                if (rootObject.permissions.x.y)
                {
                    
                }
                
                //command.Name returns the commands name
                //command.Module.Group returns the commands group
                //to check if user has permission I need to get from json storage
                //rootObject.permissions.GROUP.NAME
