import { ChevronRight } from 'lucide-react';

interface HeroProps {
  imageUrl: string;
}

export function Hero({ imageUrl }: HeroProps) {
  return (
    <section 
      className="relative bg-cover bg-center py-20 lg:py-32"
      style={{ 
        backgroundImage: `linear-gradient(rgba(26, 46, 74, 0.7), rgba(26, 46, 74, 0.7)), url(${imageUrl})` 
      }}
    >
      <div className="container mx-auto px-4">
        <div className="max-w-2xl">
          <p className="text-white/90 text-sm mb-4">Profesyonel Eğitim ve Seminerler</p>
          <h1 className="text-white mb-6">
            <div className="text-4xl lg:text-5xl font-bold mb-2">
              Kamu, Belediyeler <span className="text-white/90">ve</span>
            </div>
            <div className="text-4xl lg:text-5xl font-bold mb-4">
              İş Dünyası <span className="text-white/90">için Mevzuat,</span>
            </div>
            <div className="text-2xl lg:text-3xl text-white/90">
              Teknoloji ve Yetkinlik Programları
            </div>
          </h1>
          <p className="text-white/80 text-sm mb-8">
            Yüz Yüze - Online - Hibrit | Sertifikalı Eğitimler
          </p>
          <div className="flex flex-wrap gap-4">
            <button className="bg-orange-500 hover:bg-orange-600 text-white px-6 py-3 rounded flex items-center gap-2 transition-colors">
              Eğitimleri İncele <ChevronRight className="w-5 h-5" />
            </button>
            <button className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-3 rounded transition-colors">
              Kurumsal Teklif Al
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}
