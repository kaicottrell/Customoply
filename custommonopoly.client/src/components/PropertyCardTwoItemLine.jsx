function PropertyCardTwoItemLine({ segmentOne, segmentTwo }) {
    return (
        <div className="flex justify-between">
            <div>
                {segmentOne} 
            </div>
            <div>
                {segmentTwo}.
            </div>
        </div>
    );
}

export default PropertyCardTwoItemLine;