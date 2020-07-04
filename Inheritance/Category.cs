using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        public string productName;
        public MessageType messageType;
        public MessageTopic messageTopic;
        public Category(string productName, MessageType messageType, MessageTopic messageTopic)
        {
            this.productName = productName;
            this.messageType = messageType;
            this.messageTopic = messageTopic;
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Category newCategory = obj as Category;
            if (newCategory == null)
                throw new ArgumentException("This object is not a category");
            if (productName == null) return -1;
            var compareNumber = productName.CompareTo(newCategory.productName);
            if (compareNumber == 0)
                compareNumber = ((int)messageType).CompareTo((int)newCategory.messageType);
            else
                return compareNumber;
            if (compareNumber == 0)
                return ((int)messageTopic).CompareTo((int)newCategory.messageTopic);
            else
                return compareNumber;
        }

        public static bool operator <=(Category cat1, Category cat2)
        {
            return !(cat1 > cat2);
        }

        public static bool operator >=(Category cat1, Category cat2)
        {
            return !(cat1 < cat2);
        }
        public static bool operator <(Category cat1, Category cat2)
        {
            return (cat1.CompareTo(cat2) < 0);
        }

        public static bool operator >(Category cat1, Category cat2)
        {
            return (cat1.CompareTo(cat2) > 0);
        }

        public override bool Equals(object obj)
        {
            Category cat = obj as Category;
            if (cat == null)
                throw new ArgumentException();
            return (cat.productName == productName && cat.messageType == messageType && cat.messageTopic == messageTopic);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{productName}.{messageType}.{messageTopic}";
        }
    }

}
 