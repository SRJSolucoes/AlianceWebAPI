using System;

namespace Domain.Enum.Core
{
    /// <summary>
    ///     Classe utilizada para permitir utilizar 'String' em tipos enumerados.
    /// </summary>
    /// <remarks>
    ///     Só utilize para inteiros quando os valores não forem sequenciais ou não começarem em zero.
    /// </remarks>
    /// <example>
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
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValue : Attribute
    {
        public EnumValue(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
}