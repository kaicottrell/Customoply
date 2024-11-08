import RailroadPropertyCardContent from "./RailroadPropertyCardContent";
import BuildablePropertyCardContent from "./BuildablePropertyCardContent";
import PropertyCard from "./PropertyCard";
import UtilityCardContent from "./UtilityCardContent";
function PropertyCardWrapper({ propertyDetails }) {
    let propertyContent;
    console.log("Prop type " + propertyDetails.propertyType);
    switch (propertyDetails.propertyType) {
        case "Buildable":
            propertyContent = (
                <BuildablePropertyCardContent
                    propertyDetails={propertyDetails}
                />
            );
            break;
        case "RailRoad":
            propertyContent = (
                <RailroadPropertyCardContent
                    propertyDetails={propertyDetails}
                />
            );
            break;
        case "Utility":
            propertyContent = (
                <UtilityCardContent propertyDetails={propertyDetails} />
            )
            break;
        default:
            propertyContent = <p>No property type available.</p>;
    }

    return (
        <PropertyCard className="mb-3">
            {propertyContent}
        </PropertyCard>
    );
}


export default PropertyCardWrapper;