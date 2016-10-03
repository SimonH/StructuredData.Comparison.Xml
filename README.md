# StructuredData.Comparison.Xml
An implementation of [StructuredData.Comparison](https://github.com/SimonH/StructuredData.Comparison) for Xml (using LINQ to Xml)

Available on [Nuget]((https://www.nuget.org/packages/StructuredData.Comparison.Xml

##Usage

* Install the Nuget package (and it's dependencies)
  >install-package StructuredData.Comparison.Xml  
* Use a static function from StructuredDataComparison
 * When comparing xmlData (rather than files use the mimeType 'text/xml')

[E.g. "source xml data".Compare("comparison xml", "text/xml"); ]
