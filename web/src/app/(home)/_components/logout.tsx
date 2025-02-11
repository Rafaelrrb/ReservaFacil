import { useRouter } from "next/navigation";

export function Logout(){
  const router = useRouter();
  
  const handleLogout = async ()=>{
    if (typeof window !== "undefined") {
      // código que utiliza localStorage
      localStorage.setItem("token", "");
    }
    
    router.push('/');
  }
  

  return(
    
        <button
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          type="button"
          onClick={handleLogout}
          >
              LogOut
        </button>
    
  )
}