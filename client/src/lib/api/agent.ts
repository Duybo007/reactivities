import axios from "axios";
import { router } from "../../app/router/Routes";

const sleep=(delay: number) => {
    return new Promise(resolve => {
        setTimeout(resolve, delay)
    })
}

const agent = axios.create({
    baseURL:  import.meta.env.VITE_API_URL
});

agent.interceptors.response.use(async response => {
    await sleep(1000)
    return response
    },
    async error => {
        await sleep(1000)

        const{status, data} = error.message;
        switch (status) {
            case 400:
                if(data.error){
                    const modalSateErrors = []
                    for(const key in data.error){
                        if(data.errors[key]){
                            modalSateErrors.push(data.errors[key])
                        }
                    }
                    throw modalSateErrors.flat()
                } else {
                    // toast.error("Unauthorize")
                }
                break;
            case 401:
                // toast.error(data)
                console.log("Unauthorize")
                break;
            case 404:
                router.navigate('/not-found')
                console.log("Not found")
                break;
            case 500:
                router.navigate('/server-error', {state: {error: data}})
                break;
        
            default:
                break;
        }

        return Promise.reject(error)
    }
)

export default agent