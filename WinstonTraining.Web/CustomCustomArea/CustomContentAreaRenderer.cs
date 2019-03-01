using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WinstonTraining.Core.Models.Blocks.Interfaces;

namespace WinstonTraining.Web.CustomCustomArea
{
    public class CustomContentAreaRenderer : ContentAreaRenderer
    {
        private readonly IContentAreaLoader _contentAreaLoader;
        private readonly IContentRenderer _contentRenderer;
        private readonly IContentAreaItemAttributeAssembler _attributeAssembler;

        public CustomContentAreaRenderer(
            IContentAreaLoader contentAreaLoader,
            IContentRenderer contentRenderer,
            IContentAreaItemAttributeAssembler attributeAssembler)
        {
            _contentAreaLoader = contentAreaLoader;
            _contentRenderer = contentRenderer;
            _attributeAssembler = attributeAssembler;
        }

        //this is the exact source from the base class, code surrounded by '***' are the only deviations
        protected override void RenderContentAreaItem(
            HtmlHelper htmlHelper,
            ContentAreaItem contentAreaItem,
            string templateTag,
            string htmlTag,
            string cssClass)
        {
            var renderSettings = new Dictionary<string, object>
            {
                ["childrencustomtagname"] = htmlTag,
                ["childrencssclass"] = cssClass,
                ["tag"] = templateTag
            };

            renderSettings = contentAreaItem.RenderSettings.Concat(
                from r in renderSettings
                where !contentAreaItem.RenderSettings.ContainsKey(r.Key)
                select r).ToDictionary(r => r.Key, r => r.Value);

            htmlHelper.ViewBag.RenderSettings = renderSettings;
            var content = _contentAreaLoader.Get(contentAreaItem);
            if (content == null)
            {
                return;
            }

            //*** begin additional custom code

            var contentAsNoWrapper = content as INoWrapper;

            var renderWrapper = contentAsNoWrapper == null;

            //*** end additional custom code

            bool isInEditMode = IsInEditMode(htmlHelper);

            using (new ContentAreaContext(htmlHelper.ViewContext.RequestContext, content.ContentLink))
            {
                var templateModel = ResolveTemplate(htmlHelper, content, templateTag);

                if (templateModel != null || isInEditMode)
                {
                    if (renderWrapper)
                    {
                        var tagBuilder = new TagBuilder(htmlTag);

                        AddNonEmptyCssClass(tagBuilder, cssClass);

                        tagBuilder.MergeAttributes(_attributeAssembler.GetAttributes(
                            contentAreaItem,
                            isInEditMode,
                            templateModel != null));
                        BeforeRenderContentAreaItemStartTag(tagBuilder, contentAreaItem);
                        htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
                        htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);
                        htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.EndTag));
                    }
                    else
                    {
                        //*** don't render wrapper
                        htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);
                        //***
                    }

                }
            }
        }
    }
}