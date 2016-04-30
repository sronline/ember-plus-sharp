﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2016 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlusSharp.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    using Ember;

    /// <summary>Represents a matrix in the object tree accessible through
    /// <see cref="Consumer{T}.Root">Consumer&lt;TRoot&gt;.Root</see>.</summary>
    /// <typeparam name="TTarget">The type of the node containing the parameters of a single target.</typeparam>
    /// <typeparam name="TSource">The type of the node containing the parameters of a single source.</typeparam>
    /// <typeparam name="TConnection">The type of the node containing the parameters of a single connection.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "There's no other way.")]
    public sealed class Matrix<TTarget, TSource, TConnection> :
        ElementWithSchemas<Matrix<TTarget, TSource, TConnection>>, IMatrix
        where TTarget : Node<TTarget>
        where TSource : Node<TSource>
        where TConnection : Node<TConnection>
    {
        private MatrixType type;
        private int maximumTotalConnects;
        private int maximumConnectsPerTarget;
        private MatrixParameters<TTarget, TSource, TConnection> parameters;
        private int? gainParameterNumber;
        private IReadOnlyList<KeyValuePair<string, MatrixLabels>> labels;
        private IReadOnlyList<int> targets;
        private IReadOnlyList<int> sources;
        private IReadOnlyDictionary<int, ObservableCollection<int>> connections;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <inheritdoc/>
        public MatrixType Type
        {
            get { return this.type; }
            private set { this.SetValue(ref this.type, value); }
        }

        /// <inheritdoc/>
        public int MaximumTotalConnects
        {
            get { return this.maximumTotalConnects; }
            private set { this.SetValue(ref this.maximumTotalConnects, value); }
        }

        /// <inheritdoc/>
        public int MaximumConnectsPerTarget
        {
            get { return this.maximumConnectsPerTarget; }
            private set { this.SetValue(ref this.maximumConnectsPerTarget, value); }
        }

        /// <inheritdoc cref="IMatrix.Parameters"/>
        public MatrixParameters<TTarget, TSource, TConnection> Parameters
        {
            get { return this.parameters; }
            private set { this.SetValue(ref this.parameters, value); }
        }

        /// <inheritdoc/>
        public int? GainParameterNumber
        {
            get { return this.gainParameterNumber; }
            private set { this.SetValue(ref this.gainParameterNumber, value); }
        }

        /// <inheritdoc/>
        public IReadOnlyList<KeyValuePair<string, MatrixLabels>> Labels
        {
            get { return this.labels; }
            private set { this.SetValue(ref this.labels, value); }
        }

        /// <inheritdoc/>
        public IReadOnlyList<int> Targets
        {
            get { return this.targets; }
            private set { this.SetValue(ref this.targets, value); }
        }

        /// <inheritdoc/>
        public IReadOnlyList<int> Sources
        {
            get { return this.sources; }
            private set { this.SetValue(ref this.sources, value); }
        }

        /// <inheritdoc/>
        public IReadOnlyDictionary<int, ObservableCollection<int>> Connections
        {
            get { return this.connections; }
            private set { this.SetValue(ref this.connections, value); }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <inheritdoc/>
        INode IMatrix.Parameters
        {
            get { return this.Parameters; }
        }

        internal override RetrievalState ReadContents(EmberReader reader, ElementType actualType)
        {
            ////this.AssertElementType(ElementType.Parameter, actualType);

            ////ParameterType? valueType = null;
            ////ParameterType? enumType = null;
            ////ParameterType? typeType = null;

            ////while (reader.Read() && (reader.InnerNumber != InnerNumber.EndContainer))
            ////{
            ////    ParameterType? dummyType;

            ////    switch (reader.GetContextSpecificOuterNumber())
            ////    {
            ////        case GlowParameterContents.Description.OuterNumber:
            ////            this.Description = reader.AssertAndReadContentsAsString();
            ////            break;
            ////        case GlowParameterContents.Value.OuterNumber:
            ////            if (!this.HasChanges)
            ////            {
            ////                this.SetValue(ref this.theValue, this.ReadValue(reader, out valueType), "Value");
            ////            }

            ////            break;
            ////        case GlowParameterContents.Minimum.OuterNumber:
            ////            this.SetMinimum(this.ReadValue(reader, out dummyType));
            ////            break;
            ////        case GlowParameterContents.Maximum.OuterNumber:
            ////            this.SetMaximum(this.ReadValue(reader, out dummyType));
            ////            break;
            ////        case GlowParameterContents.Access.OuterNumber:
            ////            this.Access = this.ReadEnum<ParameterAccess>(reader, GlowParameterContents.Access.Name);
            ////            break;
            ////        case GlowParameterContents.Format.OuterNumber:
            ////            this.Format = reader.AssertAndReadContentsAsString();
            ////            break;
            ////        case GlowParameterContents.Enumeration.OuterNumber:
            ////            this.EnumMapCore = ReadEnumeration(reader);
            ////            enumType = ParameterType.Enum;
            ////            break;
            ////        case GlowParameterContents.Factor.OuterNumber:
            ////            this.FactorCore = ReadInt(reader, GlowParameterContents.Factor.Name);
            ////            break;
            ////        case GlowParameterContents.IsOnline.OuterNumber:
            ////            this.IsOnline = reader.AssertAndReadContentsAsBoolean();
            ////            break;
            ////        case GlowParameterContents.Formula.OuterNumber:
            ////            this.FormulaCore = reader.AssertAndReadContentsAsString();
            ////            break;
            ////        case GlowParameterContents.Step.OuterNumber:
            ////            ReadInt(reader, GlowParameterContents.Step.Name);
            ////            break;
            ////        case GlowParameterContents.Default.OuterNumber:
            ////            this.DefaultValue = this.ReadValue(reader, out dummyType);
            ////            break;
            ////        case GlowParameterContents.Type.OuterNumber:
            ////            typeType = this.ReadEnum<ParameterType>(reader, GlowParameterContents.Type.Name);
            ////            break;
            ////        case GlowParameterContents.StreamIdentifier.OuterNumber:
            ////            this.StreamIdentifier = ReadInt(reader, GlowParameterContents.StreamIdentifier.Name);
            ////            break;
            ////        case GlowParameterContents.EnumMap.OuterNumber:
            ////            this.EnumMapCore = this.ReadEnumMap(reader);
            ////            enumType = ParameterType.Enum;
            ////            break;
            ////        case GlowParameterContents.StreamDescriptor.OuterNumber:
            ////            this.StreamDescriptor = this.ReadStreamDescriptor(reader);
            ////            break;
            ////        case GlowParameterContents.SchemaIdentifiers.OuterNumber:
            ////            this.ReadSchemaIdentifiers(reader);
            ////            break;
            ////        default:
            ////            reader.Skip();
            ////            break;
            ////    }
            ////}

            ////this.SetFinalTytpe(valueType, enumType, typeType);
            return this.RetrievalState;
        }

        internal sealed override void WriteChanges(EmberWriter writer, IInvocationCollection pendingInvocations)
        {
            ////if (this.HasChanges)
            ////{
            ////    writer.WriteStartApplicationDefinedType(
            ////        GlowElementCollection.Element.OuterId, GlowQualifiedParameter.InnerNumber);

            ////    writer.WriteValue(GlowQualifiedParameter.Path.OuterId, this.NumberPath);
            ////    writer.WriteStartSet(GlowQualifiedParameter.Contents.OuterId);

            ////    if (this.theValue == null)
            ////    {
            ////        // This can only happen when the parameter happens to be a trigger.
            ////        writer.WriteValue(GlowParameterContents.Value.OuterId, 0);
            ////    }
            ////    else
            ////    {
            ////        this.WriteValue(writer, this.theValue);
            ////    }

            ////    writer.WriteEndContainer();
            ////    writer.WriteEndContainer();
            ////    this.HasChanges = false;
            ////}
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Matrix() : base(RetrievalState.Complete)
        {
        }
    }
}
