import PropertyCardTwoItemLine from './PropertyCardTwoItemLine.jsx';

function RailroadPropertyCardContent({ propertyDetails }) {
    return (
        <div className={`p-2 text-center flex flex-col `}>
            <div className="flex justify-center">
                <img src="https://i.pinimg.com/736x/3e/45/b0/3e45b0b90407013e4c76dda70d16d099.jpg" className="w-20 my-3" />

            </div>
            <div className={`border-y-2 border-black fw-bold text-2xl`}>
                {propertyDetails.name}.
            </div>

            <div className="text-center mt-3">
                <div className="fw-bold">
                    PURCHASE PRICE ${propertyDetails.purchasePrice}.
                </div>
                <div className="flex flex-col mt-1">
                    <PropertyCardTwoItemLine segmentOne="Rent" segmentTwo="$25" />
                    <PropertyCardTwoItemLine segmentOne="If 2 R.R.'s are owned" segmentTwo="$50" />
                    <PropertyCardTwoItemLine segmentOne="If 3 R.R.'s are owned" segmentTwo="$100" />
                    <PropertyCardTwoItemLine segmentOne="If 4 R.R.'s are owned" segmentTwo="$200" />
                </div>
                <div className="mt-2">
                    Mortgage Value ${propertyDetails.morgageValue}.
                </div>
            </div>
        </div>
    );
}


export default RailroadPropertyCardContent;