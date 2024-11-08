



function PropertyCard({ children, className }) {
    return (
        <div className={`p-4 border-2 rounded-sm shadow-2xl w-[22rem] h-[28rem] ${className}`}>
            <div className="border-2 border-black h-full flex flex-col flex items-center">
                {children}
            </div>
        </div>
    );
}
export default PropertyCard;