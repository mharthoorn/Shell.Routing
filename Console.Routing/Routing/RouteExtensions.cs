﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleRouting
{
    public static class RouteExtensions
    {
        public static IEnumerable<Route> NonDefault(this IEnumerable<Route> routes)
        {
            return routes.Where(r => !r.Default);
        }
        //public static IEnumerable<OldRoute> FindGroup(this IEnumerable<OldRoute> routes, string group)
        //{
        //    string g = group.ToLower();
        //    return routes.Where(r => string.Compare(r.Module.Title, g, ignoreCase: true) == 0);
        //}

        //public static IEnumerable<OldRoute> FindMethod(this IEnumerable<OldRoute> routes, string methodName)
        //{
        //    var method = methodName.ToLower();
        //    return routes.Where(route => route.MatchName(method));
        //}

        public static IEnumerable<Parameter> GetRoutingParameters(this IEnumerable<ParameterInfo> parameters)
        {
            foreach (var parameter in parameters)
            {
                
                yield return new Parameter
                {
                    Name = parameter.Name.ToLower(),
                    Type = parameter.ParameterType,
                    AltName = parameter.GetCustomAttribute<Alt>()?.Name,
                    Optional = parameter.HasAttribute<Optional>(),
                };
            }
        }

        public static IEnumerable<Parameter> GetRoutingParameters(this MethodInfo method)
        {
            var parameters = method.GetParameters();
            return GetRoutingParameters(parameters);
        }

        public static string Representation(this Route route)
        {
            var paraminfo = route.Method.GetParameters();
            var parameters = GetRoutingParameters(paraminfo);
            return string.Join(" ", parameters.Select(p => Representation(p)));
            
        }

        public static string Representation(this MethodInfo method)
        {
            var paraminfo = method.GetParameters();
            var parameters = GetRoutingParameters(paraminfo);
            return string.Join(" ", parameters.Select(p => Representation(p)));

        }

        public static string Representation(this Parameter parameter)
        {
            Type type = parameter.Type;
            string name = parameter.Name;

            if (type == typeof(string))
            {
                return parameter.Optional ? "(<" + name + ">)" : "<" + name + ">";
            }
            else if (type == typeof(Flag) || type == typeof(bool))
            {
                return $"--{parameter.Name}";
            }
            else if (type == typeof(Text))
            {
                return parameter.Optional ? "(<" + name + ">)" : "<" + name + ">";
            }
            else if (type == typeof(FlagValue) || type == typeof(bool))
            {
                return $"--{name} <value>";
            }
            else if (type == typeof(Arguments))
            {
                return $"({name}...)";
            }
            else return $"--- unknown: {name} ---"; // shouldn't get here.
            
        }

     
        

      
    }

}