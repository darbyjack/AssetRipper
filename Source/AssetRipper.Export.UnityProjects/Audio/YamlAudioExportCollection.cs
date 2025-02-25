﻿using AssetRipper.Assets;
using AssetRipper.Assets.Export;
using AssetRipper.Export.UnityProjects.Project.Collections;
using AssetRipper.Export.UnityProjects.Project.Exporters;
using AssetRipper.Primitives;
using AssetRipper.SourceGenerated.Classes.ClassID_83;
using AssetRipper.SourceGenerated.Extensions;
using AssetRipper.SourceGenerated.Subclasses.StreamedResource;

namespace AssetRipper.Export.UnityProjects.Audio
{
	public sealed class YamlAudioExportCollection : AssetExportCollection
	{
		public YamlAudioExportCollection(IAssetExporter assetExporter, IAudioClip asset) : base(assetExporter, asset)
		{
		}

		protected override bool ExportInner(IExportContainer container, string filePath, string dirPath)
		{
			IAudioClip asset = (IAudioClip)Asset;
			IStreamedResource? resource = asset.Resource_C83;
			if (resource is not null)
			{
				Utf8String originalSource = resource.Source;
				ulong originalOffset = resource.Offset;
				ulong originalSize = resource.Size;
				if (resource.TryGetContent(asset.Collection, out byte[]? data))
				{
					string resPath = filePath + ".resS";
					System.IO.File.WriteAllBytes(resPath, data);
					resource.Source = System.IO.Path.GetRelativePath(dirPath, resPath);
				}
				else
				{
					resource.Source = Utf8String.Empty;
					resource.Offset = 0;
					resource.Size = 0;
				}
				bool result = base.ExportInner(container, filePath, dirPath);
				resource.Source = originalSource;
				resource.Offset = originalOffset;
				resource.Size = originalSize;
				return result;
			}
			else
			{
				return base.ExportInner(container, filePath, dirPath);
			}
		}

		protected override string GetExportExtension(IUnityObjectBase asset) => "audioclip";
	}
}
