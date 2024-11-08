function CustomModal({children }) {
    return (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 ">
            <div className="bg-white p-6 rounded shadow-xl flex flex-col items-center">
              {children}
            </div>
        </div>
    );
}

export default CustomModal;