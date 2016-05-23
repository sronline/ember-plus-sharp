﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2016 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlusSharp.Model
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Ember;
    using Glow;

    /// <summary>Represents an enum parameter in the object tree accessible through
    /// <see cref="Consumer{T}.Root">Consumer&lt;TRoot&gt;.Root</see>.</summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    [SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance", Justification = "Fewer levels of inheritance would lead to more code duplication.")]
    public sealed class EnumParameter<TEnum> : ValueParameter<EnumParameter<TEnum>, TEnum>
        where TEnum : struct
    {
        internal sealed override IReadOnlyList<KeyValuePair<string, int>> EnumMapCore
        {
            get { return this.impl.EnumMapCore; }
            set { this.impl.EnumMapCore = value; }
        }

        internal sealed override RetrievalState ReadContents(EmberReader reader, ElementType actualType) =>
            this.impl.ReadContents(base.ReadContents, reader, actualType);

        internal sealed override TEnum? ReadValue(EmberReader reader, out ParameterType? parameterType)
        {
            parameterType = ParameterType.Enum;
            return FastEnum.ToEnum<TEnum>(reader.AssertAndReadContentsAsInt64());
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Method is not public, CA bug?")]
        internal sealed override void WriteValue(EmberWriter writer, TEnum? value) =>
            writer.WriteValue(GlowParameterContents.Value.OuterId, FastEnum.ToInt64(value.GetValueOrDefault()));

        internal sealed override TEnum? AssertValueType(object value)
        {
            var integer = value as long?;
            return integer.HasValue ? FastEnum.ToEnum<TEnum>(integer.Value) : base.AssertValueType(value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly EnumParameterImpl<TEnum> impl;

        private EnumParameter()
        {
            this.impl = new EnumParameterImpl<TEnum>(this);
        }
    }
}