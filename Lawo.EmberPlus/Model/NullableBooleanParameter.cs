﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com). All rights reserved.</copyright>
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System.Diagnostics.CodeAnalysis;

    using Ember;
    using Glow;

    /// <summary>Represents a nullable boolean parameter in the object tree accessible through
    /// <see cref="Consumer{T}.Root">Consumer&lt;TRoot&gt;.Root</see>.</summary>
    /// <threadsafety static="true" instance="false"/>
    [SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance", Justification = "Fewer levels of inheritance would lead to more code duplication.")]
    public sealed class NullableBooleanParameter : NullableParameter<NullableBooleanParameter, bool?>
    {
        internal sealed override bool? ReadValue(EmberReader reader, out ParameterType? parameterType)
        {
            parameterType = ParameterType.Boolean;
            return reader.AssertAndReadContentsAsBoolean();
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Method is not public, CA bug?")]
        internal sealed override void WriteValue(EmberWriter writer, bool? value)
        {
            writer.WriteValue(GlowParameterContents.Value.OuterId, value.Value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private NullableBooleanParameter()
        {
        }
    }
}
