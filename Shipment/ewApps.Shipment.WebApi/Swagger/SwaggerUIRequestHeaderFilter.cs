using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ewApps.Platform.WebApi {

  /// <summary>
  /// Operation filter to add the custom header parameters.
  /// </summary>
  public class SwaggerUIRequestHeaderFilter:IOperationFilter {

    public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context) {
      if(operation.Parameters == null)
        operation.Parameters = new List<IParameter>();

      operation.Parameters.Add(new NonBodyParameter {
        Name = "clientsessionid",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
      operation.Parameters.Add(new NonBodyParameter {
        Name = "appuserid",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
      operation.Parameters.Add(new NonBodyParameter {
        Name = "tenantid",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
      operation.Parameters.Add(new NonBodyParameter {
        Name = "appid",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
      operation.Parameters.Add(new NonBodyParameter {
        Name = "usertype",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
      operation.Parameters.Add(new NonBodyParameter {
        Name = "Authorization",
        In = "header",
        Type = "string",
        Required = false // set to false if this is optional
      });
    }
  }
}
