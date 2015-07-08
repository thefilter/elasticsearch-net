﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICommonTermsQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "low_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? LowFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "high_freq_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? HighFrequencyOperator { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "disable_coord")]
		bool? DisableCoord { get; set; }
	}

	public class CommonTermsQuery : FieldNameQuery, ICommonTermsQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Query { get; set; }
		public double? CutoffFrequency { get; set; }
		public Operator? LowFrequencyOperator { get; set; }
		public Operator? HighFrequencyOperator { get; set; }
		public string MinimumShouldMatch { get; set; }
		public double? Boost { get; set; }
		public string Analyzer { get; set; }
		public bool? DisableCoord { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.CommonTerms = this;
		internal static bool IsConditionless(ICommonTermsQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class CommonTermsQueryDescriptor<T> 
		: FieldNameQueryDescriptor<CommonTermsQueryDescriptor<T>, ICommonTermsQuery, T>
		, ICommonTermsQuery 
		where T : class
	{
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => CommonTermsQuery.IsConditionless(this);
		string ICommonTermsQuery.Query { get; set; }
		FieldName IFieldNameQuery.Field { get; set; }
		double? ICommonTermsQuery.CutoffFrequency { get; set; }
		Operator? ICommonTermsQuery.LowFrequencyOperator { get; set; }
		Operator? ICommonTermsQuery.HighFrequencyOperator { get; set; }
		string ICommonTermsQuery.MinimumShouldMatch { get; set; }
		double? ICommonTermsQuery.Boost { get; set; }
		string ICommonTermsQuery.Analyzer { get; set; }
		bool? ICommonTermsQuery.DisableCoord { get; set; }

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Boost(double boost) => Assign(a=>a.Boost = boost);

			//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> HighFrequencyOperator(Operator op) => Assign(a => a.HighFrequencyOperator = op);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> CutoffFrequency(double cutOffFrequency) => 
			Assign(a => a.CutoffFrequency = cutOffFrequency);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatch) =>
			Assign(a=>a.MinimumShouldMatch = minimumShouldMatch.ToString(CultureInfo.InvariantCulture));

		//<inheritdoc/>
		public CommonTermsQueryDescriptor<T> DisableCoord(bool disable = true) => Assign(a => a.DisableCoord = disable);
	}
}
