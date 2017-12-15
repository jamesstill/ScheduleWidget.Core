using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScheduleWidget.Common
{
    public abstract class Enumeration : IComparable
    {
        private readonly int _id;
        private readonly string _name;

        protected Enumeration() { }

        protected Enumeration(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int ID => _id;

        public string Name => _name;

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();

                if (info.GetValue(instance) is T locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = otherValue.ID.Equals(_id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.ID - secondValue.ID);
            return absoluteDifference;
        }

        public static T FromID<T>(int id) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(id, "id", item => item.ID == id);
            return matchingItem;
        }

        public static T FromName<T>(string name) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(name, "name", item => item.Name == name);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return ID.CompareTo(((Enumeration)other).ID);
        }
    }
}
