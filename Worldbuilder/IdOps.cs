using System;
using System.Linq;
using System.Reflection;

namespace Worldbuilder
{
    //TODO:Do osobnej klasy  Zamiast own uzyj "self", bledna nazwa enua powinien sie nazywacjakoś tak :  ForeginKeySource
    public enum IdOptions { Own, Objects, Related }

    public static class IdOps
    {
        //TODO:Do wyjebania puste linie

        /// <summary>
        /// Checks if a property is the id of the given type.
        /// </summary>
        /// <param name="p">Property to be checked against the type.</param>
        /// <param name="type">For own and the object's ID supply their type. For the related object supply the type with which
        /// it has a relation.</param>
        /// <param name="idOption">IdOptions: Own, Objects or Related.</param>
        /// <returns></returns>
        public static bool IsId(this PropertyInfo p, Type type, IdOptions idOption)
        {
            //TODO:Wszstkie zmienne po lewej stronie zonaczamy jako var w tym przypadku var withID          
            bool withId = p.Name.EndsWith("Id"); //TODO:Do  string do consta

            switch (idOption)
            {
                case IdOptions.Own:

                    return withId
                        //TODO:Do  2 + type.Name.Length do zmiennej 
                        && ((p.Name.StartsWith(type.Name) 
                        && p.Name.Length == 2 + type.Name.Length)
                        || (p.Name.Length == 2))
                        && (p.DeclaringType.Equals(type) 
                        || p.DeclaringType.IsAssignableFrom(type));

                case IdOptions.Objects:

                    return withId
                        && p.Name.StartsWith(type.Name)
                        && p.Name.Length == 2 + type.Name.Length
                        && !p.DeclaringType.Equals(type);

                case IdOptions.Related:

                    return withId
                        && !p.Name.StartsWith(type.Name)
                        && !p.DeclaringType.Equals(type);

                default:
                    throw new ArgumentException("Parameter 'idOption' is not set correctly");
            }
        }

        /// <summary>
        /// Finds the ID property of the type or its related object.
        /// </summary>
        /// <param name="objectsType">For the object's ID, supply their type. For the related object supply the type with which
        /// it has a relation.</param>
        /// <param name="idOption"></param>
        /// <returns></returns>
        public static PropertyInfo GetId(this Type type, Type objectsType, IdOptions idOption)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            var result = properties.FirstOrDefault(x => x.IsId(objectsType, idOption));

            if (result == null) throw new ArgumentException("Given type does not have any Id property following EF Core naming rules.");
            else return result;

        }
        /// <summary>
        /// Finds the ID property in a given type.
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo GetId(this Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            var result = properties.FirstOrDefault(x => x.IsId(type, IdOptions.Own));

            if (result == null) throw new ArgumentException("Given type does not have any Id property following EF Core naming rules.");
            else return result;

        }
        /// <summary>
        /// Checks if a type has and id property of the type given in the parameter.
        /// </summary>
        /// <param name="type">For own and the object's ID supply their type. For the related object supply the type with which
        /// it has a relation.</param>
        /// <param name="idOption"></param>
        /// <returns></returns>
        static public bool HasId(this Type type, IdOptions idOption = IdOptions.Own)
        {
            return type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            .Any(p => p.IsId(type, idOption));
        }


    }
}
