using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Exceptions
{
    public class TransactionException : Exception
    {
        /// <summary>
        /// Just create the exception
        /// </summary>
        public TransactionException()
            : base()
        {}

        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public TransactionException(String message)
            : base(message) {
        }

        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="innerException">Exception inner cause</param>
        public TransactionException(String message, Exception innerException)
            : base(message, innerException) {
        }

        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        protected TransactionException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }




        /// <summary>
        /// Using Errors enumerator
        /// </summary>
        public TransactionException(Errors_Transaction errors_Transaction)
            : base(GetEnumDescription(errors_Transaction))
        {   
        }
        private static string GetEnumDescription(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    public enum Errors_Transaction
    {
        [Description("Error al insertar los datos")]
        ERR_TRANSACTION_100_SAVE,
        [Description("El saldo de la cuenta no puede ser menor al monto a transferir")]
        ERR_TRANSACTION_101_UPDATE        
    }
    }
}
