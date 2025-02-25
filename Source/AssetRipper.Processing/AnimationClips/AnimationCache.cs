﻿using AssetRipper.Assets;
using AssetRipper.Assets.Bundles;
using AssetRipper.SourceGenerated.Classes.ClassID_111;
using AssetRipper.SourceGenerated.Classes.ClassID_90;
using AssetRipper.SourceGenerated.Classes.ClassID_95;

namespace AssetRipper.Processing.AnimationClips;

public sealed class AnimationCache
{
	public IReadOnlyList<IAvatar> CachedAvatars { get; }
	public IReadOnlyList<IAnimator> CachedAnimators { get; }
	public IReadOnlyList<IAnimation> CachedAnimations { get; }

	private AnimationCache(Bundle bundle)
	{
		List<IAvatar> cachedAvatars = new();
		List<IAnimator> cachedAnimators = new();
		List<IAnimation> cachedAnimations = new();
		foreach (IUnityObjectBase asset in bundle.FetchAssetsInHierarchy())
		{
			switch (asset)
			{
				case IAvatar avatar:
					cachedAvatars.Add(avatar);
					break;
				case IAnimator animator:
					cachedAnimators.Add(animator);
					break;
				case IAnimation animation:
					cachedAnimations.Add(animation);
					break;
			}
		}
		CachedAvatars = cachedAvatars;
		CachedAnimators = cachedAnimators;
		CachedAnimations = cachedAnimations;
	}

	public static AnimationCache CreateCache(Bundle bundle)
	{
		return new AnimationCache(bundle);
	}
}
