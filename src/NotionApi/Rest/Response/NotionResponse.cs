﻿using Util;

namespace NotionApi.Rest.Response;

public class NotionResponse<TResponseType> : INotionResponse<TResponseType>
{
    public Option<TResponseType> Result { get; }

    public NotionResponse(TResponseType value)
    {
        Result = value;
    }
}