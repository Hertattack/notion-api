# Notion Graph Database Example

## Query language

```
// Give me all books with tag with title Leadership and their tags.

(tags{Title:"Leadership"})-[book]->(b:books)-[tag]->(tb:tags)
RETURN b.*, tb.*
```

```json
{
    "Metadata" : {
        "$1" : {
            "Type": "Node",
            "Class": "books"
        },
        "$2" : {
            "Type": "Node",
            "Class": "tags"
        }
    },
    "Results" : [
        {
            "Metadata" : "$1",
            "Properties" : {
                "Title" : "<book title>",
                "..." : "..."
            },
            "Children" : {
                "tag" : {
                    "Nodes" : [
                        {
                            "Metadata" : "$2",
                            "Properties" : {
                                "...": "..."
                            }
                        }
                    ]
                }
            }
        }
    ]
}
```

## Using

* [csly](https://github.com/b3b00/csly) for the parser generation.