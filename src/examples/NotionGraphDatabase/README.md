# Notion Graph Database Example

## Query language

```
// Give me all books with tag with title Leadership and their tags.
tags-[book]->books
(tags{Title:"Leadership"})-[book]>(b:books)-[tag]>(tb:tags)
RETURN b.*, tb.*
```

The following operators can be used:

* `>` greater than
* `>=` greater or equal
* `<` less than
* `<=` less or equal
* `=` equals
* `!` not
* `~=` contains
* `?=` starts with
* `=?` ends with

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