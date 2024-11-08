import PropertyCardTwoItemLine from "./PropertyCardTwoItemLine";

function UtilityCardContent({ propertyDetails }) {
    const isWaterWorks = propertyDetails.name.includes("Water");
    const utilityImgLink = isWaterWorks ? "../assets/images/UtilLightBulb.png" : "data:image/png;base64,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"
    return (
        <div className={`p-2 text-center flex flex-col `}>
            <div className="flex justify-center">
                <img src={ utilityImgLink } className="w-20 my-3" />
            </div>
            <div className={`border-y-2 border-black fw-bold text-2xl`}>
                {propertyDetails.name}.
            </div>

            <div className="text-center mt-3">
                <div className="fw-bold">
                    PURCHASE PRICE ${propertyDetails.purchasePrice}.
                </div>
                <div className="flex flex-col mt-1">
                    <p>
                        If one "Utility is owned" rent is 4 times amount shown on dice
                    </p>
                    <p>
                        If both "Utility is owned" rent is 10 times amount shown on dice
                    </p>
                </div>
                <div className="mt-2">
                    <PropertyCardTwoItemLine segmentOne="Mortgage Value" segmentTwo={`${propertyDetails.morgageValue}`} />
                </div>
            </div>
        </div>
    );
}

export default UtilityCardContent;