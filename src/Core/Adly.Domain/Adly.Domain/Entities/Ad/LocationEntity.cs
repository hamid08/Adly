using Adly.Domain.Common;

namespace Adly.Domain.Entities.Ad;

public sealed class LocationEntity:BaseEntity<Guid>
{
    public string Name { get; private set; }

    private List<AdEntity> _ads = new();

    public IReadOnlyList<AdEntity> Ads => _ads.AsReadOnly();

    public LocationEntity(string name)
    {
        Name = name;
    }
}