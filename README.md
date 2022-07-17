# Notion API for C#

[![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/Hertattack/notion-api)

This is my tinkering with the [Notion API](https://developers.notion.com/) in C#. Feel free to use the code at your own
risk :-) I have been using Notion for a while and wanted to see if I could make my notes even more useful for me.

My initial goal is to visualize the connections I have made between my notes in a graph. See
the [Notion Visualizer](src/examples/NotionVisualizer/README.md) example's readme for more information on this.

Since I like programming I went a bit overboard and did not take the shortest path to the solution but created some
libraries and a [Notion API Client](src/NotionApi/README.md) that I can expand as the API gets more functionality.

The components relate to each other in the following way

```mermaid
C4Container

title Context diagram for the Notion API visualization components

Boundary(visualizationSystem, "Notion Visualization System", "System") {
    Boundary(graphApiBoundary, "Notion Graph API", "System") {
        Component(graphDatabase, "Notion Graph Database")
        Component(notionApi, "Notion API Client")
        Component(restClient, "Rest Client")
        Component(util, "Utilities")
    }
}

Boundary(frontend, "Notion Explorer", "System") {
    Component(explorerFrontend, "Notion Explorer Frontend")
}

System_Ext(notion, "Notion API")

Rel(restClient, util, "Uses")
Rel(notionApi, restClient, "Uses")
Rel(graphDatabase, notionApi, "Uses")

Rel(graphApiBoundary, notion, "Uses")
```

