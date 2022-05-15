# Notion API Client

This is a basic Notion API client written in C#.

## Limitations

There are still many limitations. The API only supports the generated token as
described [here](https://www.notion.so/my-integrations). You need to add a private integration there. This will give you
a token which you can then add to the configuration. See
the [appsettings.json](../examples/NotionVisualizer/appsettings.json) file in the examples folder.
