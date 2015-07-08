﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateOperation
	{

		string Id { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty(PropertyName = "score")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		IDictionary<FieldName, ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		QueryContainer Filter { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		IRequestParameters GetRequestParameters();
	
	}
}