using System;
using System.Linq;
using System.Resources;
using System.Threading;

namespace Domain.Enum.Core
{
    public static class EnumUtility
    {
        private const short ENUM_LENGTH = 1;
        private const short ENUM_POSITION = 0;

        public static string GetDescription(this System.Enum item)
        {
            var description = (EnumDescription[])item.GetType().GetCustomAttributes(typeof(EnumDescription), false);

            if (description.Length == ENUM_LENGTH)
            {
                var resourceManager = new ResourceManager(description[ENUM_POSITION].ResourceType);
                var message = resourceManager.GetString(item.ToString(), Thread.CurrentThread.CurrentCulture);
                return !string.IsNullOrEmpty(message) ? message : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Método de extensão para classe Enum utilizado para obter o valor de um tipo enumerado que possui o atributo
        ///     'EnumValue'.
        /// </summary>
        /// <example>
        ///     1) Criar um tipo enumerado e colocar o atributo 'EnumValue' em cima de cada , como no exemplo abaixo:
        ///     public enum StatusPedidoCompra
        ///     {
        ///     [EnumValue("E")]
        ///     EmAprovacao,
        ///     [EnumValue("A")]
        ///     Aprovado,
        ///     [EnumValue("R")]
        ///     Reprovado,
        ///     [EnumValue("T")]
        ///     TotalmenteAtendido,
        ///     [EnumValue("P")]
        ///     ParcialmenteAtendido,
        ///     [EnumValue("F")]
        ///     AtendidoComFalta
        ///     }
        ///     2) Depois, para obter o valor, basta utilizar o [enumerador].[posicao].GetValue(), conforme exemplo abaixo:
        ///     StatusPedidoCompra.EmAprovacao.GetValue().ToString();
        /// </example>
        /// <param name="item"></param>
        /// <returns>Retorna o valor do tipo enumerado</returns>
        public static object GetValue(this System.Enum item)
        {
            var enumValue = (EnumValue[])item.GetType().GetField(item.ToString())
                .GetCustomAttributes(typeof(EnumValue), false);

            if (enumValue.Length == ENUM_LENGTH) return enumValue[ENUM_POSITION].Value;

            return null;
        }

        /// <summary>
        ///     Método para obter o Enum a partir do seu valor.
        /// </summary>
        /// <example>
        ///     1) Criar um tipo enumerado e colocar o atributo 'EnumValue' em cima de cada , como no exemplo abaixo:
        ///     public enum StatusPedidoCompra
        ///     {
        ///     [EnumValue("E")]
        ///     EmAprovacao,
        ///     [EnumValue("A")]
        ///     Aprovado,
        ///     [EnumValue("R")]
        ///     Reprovado,
        ///     [EnumValue("T")]
        ///     TotalmenteAtendido,
        ///     [EnumValue("P")]
        ///     ParcialmenteAtendido,
        ///     [EnumValue("F")]
        ///     AtendidoComFalta
        ///     }
        ///     2) Depois, para obter o enum a partir do valor, basta utilizar o EnumUtility.GetEnumByValue([valor]), conforme
        ///     exemplo abaixo:
        ///     EnumUtility.GetEnumByValue("E");
        /// </example>
        /// <typeparam name="T">Tipo enumerado</typeparam>
        /// <param name="value">Valor a ser considerado na busca</param>
        /// <returns>Retorna um Enum correspondente ao valor</returns>
        public static System.Enum GetEnumByValue<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

            var enumTexts = typeof(T).GetEnumValues();

            foreach (var enumText in enumTexts)
            {
                var enumValue = (EnumValue[])enumText.GetType().GetField(enumText.ToString())
                    .GetCustomAttributes(typeof(EnumValue), false);

                if (enumValue.Count() > 0 &&
                    enumValue.Where(i => i.Value.ToString() == value).FirstOrDefault() != null)
                    return (System.Enum)enumText;
            }

            return null;
        }


        /// <summary>
        ///     Método para obter a descrição de um Enum a partir do seu valor (EnumValue).
        /// </summary>
        /// <example>
        ///     1) Criar um tipo enumerado e colocar o atributo 'EnumValue' em cima de cada , como no exemplo abaixo:
        ///     public enum StatusPedidoCompra
        ///     {
        ///     [EnumValue("E")]
        ///     EmAprovacao,
        ///     [EnumValue("A")]
        ///     Aprovado,
        ///     [EnumValue("R")]
        ///     Reprovado,
        ///     [EnumValue("T")]
        ///     TotalmenteAtendido,
        ///     [EnumValue("P")]
        ///     ParcialmenteAtendido,
        ///     [EnumValue("F")]
        ///     AtendidoComFalta
        ///     }
        ///     2) Depois, para obter a descrição a partir do valor, basta utilizar o EnumUtility.GetDescriptionByValue([valor]),
        ///     conforme exemplo abaixo:
        ///     EnumUtility.GetDescriptionByValue("E");
        /// </example>
        /// <typeparam name="T">Tipo enumerado</typeparam>
        /// <param name="value">Valor a ser considerado na busca</param>
        /// <returns>Retorna a descrição correspondente ao valor</returns>
        public static string GetDescriptionByValue<T>(string value)
        {
            var enumElement = GetEnumByValue<T>(value);

            if (enumElement == null) return null;

            return enumElement.GetDescription();
        }
    }
}