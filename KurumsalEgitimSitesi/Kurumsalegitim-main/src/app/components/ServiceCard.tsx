import { ChevronRight, LucideIcon } from 'lucide-react';

interface ServiceCardProps {
  icon: LucideIcon;
  title: string;
  description: string;
  buttonColor: string;
  bgColor: string;
}

export function ServiceCard({ icon: Icon, title, description, buttonColor, bgColor }: ServiceCardProps) {
  return (
    <div className={`${bgColor} rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow`}>
      <div className="flex items-start gap-4 mb-4">
        <div className="bg-white rounded-lg p-3 shadow-md">
          <Icon className="w-8 h-8 text-blue-600" />
        </div>
        <h3 className="text-xl font-semibold text-gray-800 mt-2">{title}</h3>
      </div>
      <p className="text-gray-600 text-sm mb-6">{description}</p>
      <button className={`${buttonColor} text-white px-6 py-3 rounded flex items-center gap-2 hover:opacity-90 transition-opacity`}>
        Tüm Eğitimlere Gör <ChevronRight className="w-5 h-5" />
      </button>
    </div>
  );
}
