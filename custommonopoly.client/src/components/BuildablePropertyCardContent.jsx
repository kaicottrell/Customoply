import {boardSquareColors} from '../constants/boardConstants';
import PropertyCardTwoItemLine from './PropertyCardTwoItemLine.jsx';

function BuildablePropertyCardContent({ propertyDetails }) {
    const propertyColorClass = boardSquareColors[propertyDetails.color];

    return (
        <div className={`p-2 text-center flex flex-col `}>
            <div className={`${propertyColorClass} border-2 border-black`}>
                <div className="text-center text-sm fw-bold">
                    T I T L E  D E E D
                </div>
                <h3 className="text-center">
                    {propertyDetails.name}.
                </h3>
            </div>
           
            <div className="text-center">
                <div className="fw-bold">
                    PURCHASE PRICE ${propertyDetails.purchasePrice}.
                </div>
                <div>
                    RENT ${propertyDetails.rentNoHouse}.
                </div>
                <div className="flex flex-col mt-1">
                    <PropertyCardTwoItemLine segmentOne="With 1 House" segmentTwo={propertyDetails.rentOneHouse} />
                    <PropertyCardTwoItemLine segmentOne="With 2 House" segmentTwo={propertyDetails.rentTwoHouse} />
                    <PropertyCardTwoItemLine segmentOne="With 3 House" segmentTwo={propertyDetails.rentThreeHouse} />
                    <PropertyCardTwoItemLine segmentOne="With 4 House" segmentTwo={propertyDetails.rentFourHouse} />
                </div>
                <div>
                    With Hotel ${propertyDetails.rentHotel}.
                </div>
                <div className="mt-2">
                    Mortgage Value ${propertyDetails.morgageValue}.
                </div>
            </div>
            <div>
                Houses Cost ${propertyDetails.houseHotelCost}. each
            </div>
            <div>
                Hotels, ${propertyDetails.houseHotelCost}. plus 4 houses
            </div>
            <div className="text-xs">
                If a player owns ALL the Lots of any Color-Group, the rent is Doubled on Unimproved Lots in that group
            </div>
        </div>
    );
}


export default BuildablePropertyCardContent;